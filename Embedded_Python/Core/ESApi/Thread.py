import kivy.clock
import threading


class Thread:
    """
        Wraps pythons threading system in a more OOP way and uses kivy
        to provide a callback on the main thread if the started thread has finished
    """

    __thread = None  # type: Thread
    __running = False  # type: bool
    __callback = None

    def __init__(self):
        """
            Init the thread
        """

        self.__thread = threading.Thread(target=self._internal_run)

    def set_callback(self, callback):
        """
            Sets the callback thats being called on the main thread after the started thread has finished
        :param callback: Callback to call on main thread
        """

        self.__callback = callback

    def start(self):
        """
            Starts the thread
        """

        self.__running = True
        self.__thread.start()

    def _internal_run(self):
        """
            Internal function that gets run by pythons threading system
        """

        self._run()
        self.__running = False
        kivy.clock.ClockBase.schedule_once(kivy.clock.Clock, self._internal_callback)

    def _internal_callback(self, dt):
        """
            Internal callback that gets called by kivy after thread has finished
        :param dt: Delta Time
        """

        if self.__callback is None:
            return

        self.__callback()

    def _run(self):
        """
            Method that gets implemented by the threads subclass. This Method will
            be run on another thread
        """

        raise NotImplementedError()