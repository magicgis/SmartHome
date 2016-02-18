import ESApi.Networking as Networking
from ESApi.Application import Application
from ESApi.AppScreen import AppScreen
from ESApi.ServerProvider import ConnectionIdentifier, ServerProvider
from ESApi.IXPFile import IXPFile

import LightSrc.LightNetworking as LightNetworking
from LightSrc.UI.LightSelection import LightSelection

app = None  # type: LightApp


class _ScreenController:
    def get_screen(self) -> AppScreen:
        raise NotImplementedError()


class LightApp(Application):
    lightSelection = None  # type: _LightSelectionController

    __curScreen = None  # type: AppScreen
    __bootStepsNeeded = 1
    __bootStepsDone = 0
    __bootCallable = None

    def on_system_boot(self, finishedCallback: callable):
        instance = self
        self.__bootCallable = finishedCallback
        global app
        app = self
        LightNetworking.instance.connect()
        self.lightSelection = _LightSelectionController()
        self.__curScreen = self.lightSelection.get_screen()
        self.do_boot_step()

    def get_current_screen(self) -> AppScreen:
        return self.__curScreen

    def switch_screen(self, controller: _ScreenController):
        self.__curScreen = controller.get_screen()
        self.refresh_screen()

    def add_boot_step(self):
        self.__bootStepsNeeded = self.__bootStepsNeeded + 1

    def do_boot_step(self):
        self.__bootStepsDone = self.__bootStepsDone + 1

        if self.__bootStepsDone < self.__bootStepsNeeded:
            return;

        self.__bootCallable()


class _LightSelectionController(_ScreenController):
    __screen = None  # type: LightSelection
    __lights = {}  # type: dic

    def __init__(self):
        self.__screen = LightSelection()
        con = LightNetworking.instance.get_connection()  # type: ConnectionIdentifier
        server = Networking.instance.get_server()
        getLightsRequest = IXPFile()
        getLightsRequest.set_network_function("com.projectgame.huelightning.hue.getlights")
        server.ixp_request_async(con, getLightsRequest, self.__network_lights_arrived)
        global app # type: LightApp
        app.add_boot_step()

    def __network_lights_arrived(self, ixpFile: IXPFile = None):
        global app
        con = LightNetworking.instance.get_connection()
        server = Networking.instance.get_server()
        count = int(ixpFile.get_info("Count"))

        for index in range(0, count):
            id = int(ixpFile.get_info("L" + str(index)))
            self.__lights[id] = ""
            getLightNameReq = IXPFile()
            getLightNameReq.set_network_function("com.projectgame.huelightning.hue.getlightname")
            getLightNameReq.add_info("light_id", str(id))
            server.simple_string_request_async(con, getLightNameReq, lambda response: self.__network_light_name_arrived(id, response))
            app.add_boot_step()

        app.do_boot_step()


    def __network_light_name_arrived(self, id: int, response: str = None):
        global app
        self.__lights[id] = response
        self.__screen.add_light(response, self.light_selected)
        app.do_boot_step()


    def light_selected(self):
        pass

    def get_screen(self):
        return self.__screen