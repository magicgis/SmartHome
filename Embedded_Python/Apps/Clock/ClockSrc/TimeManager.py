import ESApi.Networking as Networking
import ClockSrc.ClockNetworking as ClockNetworking

from ESApi.ServerProvider import ConnectionIdentifier, ServerProvider
from ESApi.IXPFile import IXPFile


class TimeManager:
    __hours = -1  # type: int
    __minutes = -1  # type: int
    __seconds = -1  # type: int

    __callbacks = []  # type: List[callable]
    __dateCallbacks = []

    def start(self):
        server = Networking.instance.get_server()
        con = ClockNetworking.instance.get_connection()
        server.register_message_listener(con, "clock_time_listener", self.time_listener)

        request = IXPFile()
        request.set_network_function("com.projectgame.clock.clock.registertotimeservice")
        request.add_info("functionName", "clock_time_listener")
        server.no_response_request(con, request)

    def register_time_listener(self, callback):
        """
            Registers a listener that gets calles when the time changes
        :param callback: Callable with a the following optional parameters:
                         - hours: int = -1
                         - minutes: int = -1
                         - seconds: int = -1
        """

        self.__callbacks.append(callback)

    def unregister_time_listener(self, callback):
        """
            Unregisters a listener from the TimeManager
        :param callback: The callback to unregister
        """

        self.__callbacks.remove(callback)

    def register_date_listener(self, callback):
        self.__dateCallbacks.append(callback)

    def unregister_date_listener(self, callback):
        self.__callbacks.remove(callback)

    def time_listener(self, message: IXPFile = None):
        sH = message.get_info("hours")
        sM = message.get_info("minutes")
        sS = message.get_info("seconds")

        iH = int(sH)
        iM = int(sM)
        iS = int(sS)

        needsDateUpdate = False

        if self.__hours == -1:
            needsDateUpdate = True

        if self.__hours != 0 and iH == 0:
            needsDateUpdate = True

        if needsDateUpdate:
            server = Networking.instance.get_server()
            con = ClockNetworking.instance.get_connection()
            request = IXPFile()
            request.set_network_function("com.projectgame.clock.clock.getdate")
            server.ixp_request_async(con, request, lambda ixpFile: self.__date_listener(ixpFile))

        self.__set_time(hours=iH, minutes=iM, seconds=iS)

    def __date_listener(self, ixpFile: IXPFile):
        sY = ixpFile.get_info("year")
        sM = ixpFile.get_info("month")
        sD = ixpFile.get_info("day")
        sW = ixpFile.get_info("weekday")

        iY = int(sY)
        iM = int(sM)
        iD = int(sD)
        iW = int(sW)

        if iW == -1:
            iW = 6

        for listener in self.__dateCallbacks:
            listener(year=iY, month=iM, day=iD, weekday=iW)

    def __set_time(self, hours: int, minutes: int, seconds: int):
        timeChanges = False

        if hours != self.__hours:
            timeChanges = True

        if minutes != self.__minutes:
            timeChanges = True

        if seconds != self.__seconds:
            timeChanges = True

        if timeChanges == False:
            return

        self.__hours = hours
        self.__minutes = minutes
        self.__seconds = seconds

        for listener in self.__callbacks:
            listener(hours=self.__hours, minutes=self.__minutes, seconds=self.__seconds)

instance = TimeManager()