import ESApi.Networking as Networking
from ESApi.Application import Application
from ESApi.AppScreen import AppScreen
from ESApi.ServerProvider import ConnectionIdentifier, ServerProvider
from ESApi.IXPFile import IXPFile

import time

import LightSrc.LightNetworking as LightNetworking
from LightSrc.UI.LightSelection import LightSelection
from LightSrc.UI.LightData import LightData

from datetime import datetime

app = None  # type: LightApp


class _ScreenController:
    def get_screen(self) -> AppScreen:
        raise NotImplementedError()


class LightApp(Application):
    lightSelection = None  # type: _LightSelectionController
    lightData = None  # type _LightDataController

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
        self.lightData = _LightDataController()
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
            time.sleep(0.5)
            id = int(ixpFile.get_info("L" + str(index)))
            self.__lights[id] = ""
            getLightNameReq = IXPFile()
            getLightNameReq.set_network_function("com.projectgame.huelightning.hue.getlightname")
            getLightNameReq.add_info("light_id", str(id))
            server.simple_string_request_async(con, getLightNameReq, self.__get_name_lambda(id))
            app.add_boot_step()

        app.do_boot_step()

    def __get_name_lambda(self, id: int):
        return lambda response: self.__network_light_name_arrived(id, response)

    def __light_selected_lambda(self, id: int):
        return lambda: self.light_selected(id)

    def __light_data_lambda(self, id: int):
        return lambda response: self.__network_light_data_arrived(id, response)

    def __network_light_name_arrived(self, id: int, response: str = None):
        global app
        self.__lights[id] = response
        self.__screen.add_light(response, self.__light_selected_lambda(id))
        app.do_boot_step()

    def __network_light_data_arrived(self, id: int, response: IXPFile = None):
        global app

        hue = int(response.get_info("hue"))
        sat = int(response.get_info("sat"))
        bri = int(response.get_info("bri"))

        app.lightData.update(id, hue, sat, bri)
        app.switch_screen(app.lightData)

    def light_selected(self, id : int):
        con = LightNetworking.instance.get_connection()  # type: ConnectionIdentifier
        server = Networking.instance.get_server()
        getLightColor = IXPFile()
        getLightColor.set_network_function("com.projectgame.huelightning.hue.getlightcolor")
        getLightColor.add_info("light_id", str(id))
        server.ixp_request_async(con, getLightColor, self.__light_data_lambda(id))

    def get_screen(self):
        return self.__screen

class _LightDataController(_ScreenController):
    __screen = None  # type: LightData
    __id = None  # type: int
    __delay = -1  # type: int

    def __init__(self):
        self.__screen = LightData(self.__data_changed)

    def update(self, id: int, hue: int = 0, sat: int = 255, bri: int = 255):
        self.__id = id
        self.__screen.update_data(hue, sat, bri)

    def get_screen(self) -> AppScreen:
        return self.__screen

    def __data_changed(self, hue: int = 0, sat: int = 0, bri: int = 0):
        curTimeStamp = datetime.now().time()

        if curTimeStamp <= self.__delay:
            return

        self.__delay = curTimeStamp + 1

        con = LightNetworking.instance.get_connection()  # type: ConnectionIdentifier
        server = Networking.instance.get_server()
        setLightColor = IXPFile()
        setLightColor.set_network_function("com.projectgame.huelightning.hue.setlightcolor")
        setLightColor.add_info("light_id", str(self.__id))
        setLightColor.add_info("hue", str(hue))
        setLightColor.add_info("sat", str(sat))
        setLightColor.add_info("bri", str(bri))
        server.no_response_request(con, setLightColor)