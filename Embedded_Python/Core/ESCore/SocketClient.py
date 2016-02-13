import socket
from socket import error as socket_error
from functools import partial
from select import select
import time

import kivy.clock

from ESApi.Thread import Thread


class _SocketListener(Thread):
    __socket = None  # type: socket
    __size = -1  # type: int

    __callbacks = []  # type: List

    __running = True  # type: bool

    def __init__(self, socket: socket.socket, size: int):
        super(_SocketListener, self).__init__()
        self.__socket = socket
        self.__size = size

    def register_callback(self, callback: callable):
        self.__callbacks.append(callback);

    def stop(self):
        self.__running = False

    def _run(self):
        try:
            while self.__running is True:
                r, _, _ = select([self.__socket], [], [])
                if r:
                    data = self.__socket.recv(self.__size)  # type: bytes

                    if not data:
                        continue

                    decoded = data.decode()

                    kivy.clock.ClockBase.schedule_once(kivy.clock.Clock, partial(self._listener_internal_callback, decoded))

                time.sleep(0.5)
        except socket_error as serr:
            return


    def _listener_internal_callback(self, data: str, *largs):
        for listener in self.__callbacks:
            listener(data)


class SocketClient:
    __host = None  # type: str
    __port = -1  # type: int
    __size = -1  # type: int

    __socket = None  # type: socket
    __listener = None  # type: __SocketListener

    def __init__(self, host: str, port: int, size: int = 1024):
        self.__host = host
        self.__port = port
        self.__size = size
        self.__socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.__listener = _SocketListener(self.__socket, self.__size)

    def connect(self):
        self.__socket.connect((self.__host, self.__port))
        self.__listener.start()

    def close(self):
        self.__listener.stop()
        self.__socket.close()

    def register_callback(self, callback: callable):
        self.__listener.register_callback(callback)

    def write(self, data: str):
        self.__socket.send(data.encode(encoding='utf_8'))