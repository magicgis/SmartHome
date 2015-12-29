import kivy.clock
import threading


class Thread:
    __thread = None  # type: Thread
    __running = False  # type: bool
    __callback = None

    def __init__(self):
        self.__thread = threading.Thread(target=self._internal_run)

    def set_callback(self, callback):
        self.__callback = callback

    def start(self):
        self.__running = True
        self.__thread.start()

    def _internal_run(self):
        self._run()
        self.__running = False
        kivy.clock.ClockBase.schedule_once(kivy.clock.Clock, self._internal_callback)

    def _internal_callback(self, dt):
        self.__callback()

    def _run(self):
        raise NotImplementedError()