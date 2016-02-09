from kivy.app import Widget
from kivy.uix.image import Image

from ESCore.UI.SingleApplicationScreenWidget import SingleApplicationScreenWidget
from ESCore.Controller.Controller import Controller
from ESCore.UI.AppLauncherWidget import AppLauncherWidget


class AppLauncherController(Controller):
    """
        Handles the graphics of the app launcher screen
    """

    __widget = None  # type: SingleApplicationWidget
    __child = None  # type: AppLauncherWidget
    __image_widget = None  # type: Image

    def initialize(self):
        self.__child = AppLauncherWidget()
        self.__widget = SingleApplicationScreenWidget(self.__child, useBottomBar = True, useTopBar = False)

    def get_widget(self) -> Widget:
        """
            override of superclass method
        """

        return self.__widget

    def on_set(self):
        """
            override of superclass method
        """

        pass

    def on_unset(self):
        """
            override of superclass method
        """

        pass

instance = AppLauncherController()