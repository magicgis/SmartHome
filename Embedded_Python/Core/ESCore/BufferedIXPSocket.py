from functools import partial
import gc
import uuid

import kivy.clock

from ESCore.SocketClient import SocketClient
from ESApi.IXPFile import IXPFile


class BufferedIXPSocket:
    __IXP_START = "<?xml version"
    __IXP_END = "</IXP>"

    __client = None  # type: SocketClient
    __callbacks = []  # type: List[]
    __buffer = ''  # type: str

    def __init__(self, host: str, port: int):
        self.__client = SocketClient(host, port)
        self.__client.register_callback(self._message_received)
        self.__client.connect()

    def stop(self):
        self.__client.close()

    def register_callback(self, callback: callable):
        self.__callbacks.append(callback)

    def send(self, ixpFile: IXPFile):
        self.__client.write(ixpFile.get_xml())

    def _message_received(self, data: str):
        self.__buffer += data
        self.__search_buffer()

    def _internal_callback(self, ixpFile: IXPFile, *largs):
        for callback in self.__callbacks:
            callback(ixpFile)

    def __search_buffer(self):
        bufferLength = len(self.__buffer)

        for startIndex in range(0, bufferLength - len(self.__IXP_START) - len(self.__IXP_END) + 1):
            startSub = self.__buffer[startIndex:startIndex + len(self.__IXP_START)]

            if startSub != self.__IXP_START:
                continue

            for endIndex in range(startIndex + len(self.__IXP_START), bufferLength - len(self.__IXP_END) + 1):
                endSub = self.__buffer[endIndex:endIndex + len(self.__IXP_END)]

                if endSub != self.__IXP_END:
                    continue

                xml = self.__buffer[startIndex : endIndex + len(self.__IXP_END)]
                self.__remove_from_buffer(startIndex, endIndex + len(self.__IXP_END))
                self.__found_message(xml)
                self.__search_buffer()
                return

    def __remove_from_buffer(self, start: int, end: int):
        before = self.__buffer[0:start]
        after = self.__buffer[end + 1:len(self.__buffer)]
        self.__buffer = before + after

    def __found_message(self, data: str):
        ixp = IXPFile(source = data)
        kivy.clock.ClockBase.schedule_once(kivy.clock.Clock, partial(self._internal_callback, ixp))