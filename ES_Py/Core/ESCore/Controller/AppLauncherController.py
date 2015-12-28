from kivy.app import Widget
from kivy.uix.image import Image

import kivy.clock

from ESCore.UI.SingleApplicationScreenWidget import SingleApplicationScreenWidget
from ESCore.Controller.Controller import Controller

import ESCore.CoreFileIO as FileIO

class AppLauncherController(Controller):
    __widget = None  # type: SingleApplicationWidget
    __image_widget = None  # type: Image

    def initialize(self):
        childWidget = Image()
        self.__image_widget = childWidget
        kivy.clock.ClockBase.schedule_once(kivy.clock.Clock, self._load_image)
        childWidget.allow_stretch = True
        childWidget.keep_ratio = False
        self.__widget = SingleApplicationScreenWidget(childWidget, useBottomBar = True, useTopBar = False)

    def _load_image(self, dt: int = 0):
        self.__image_widget.source = FileIO.sysdata_directory() + "/launcher/background.png"

    def get_widget(self) -> Widget:
        return self.__widget

    def on_set(self):
        pass
    def on_unset(self):
        pass

instance = AppLauncherController()