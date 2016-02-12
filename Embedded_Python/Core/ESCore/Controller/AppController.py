from kivy.app import Widget

from ESCore.Controller.Controller import Controller
from ESCore.CoreApplication import CoreApplication
from ESCore.UI.SingleApplicationScreenWidget import SingleApplicationScreenWidget
from ESCore.UI.SingleApplicationScreenSubWidget import SingleApplicationScreenSubWidget

class AppController(Controller):
    """
        Handles displaying apps and their corresponding screens
    """

    __app = None  # type: CoreApplication
    __widget = None  # type: SingleApplicationScreenWidget

    __tmpCurrentScreen = None  # type: SingleApplicationScreenSubWidget

    def __init__(self, app: CoreApplication):
        """
        :param app: The app this controller has to display
        """

        self.__app = app

    def get_widget(self) -> Widget:
        """
            override of superclass method
        """

        subWidget = self.__app.app().get_current_screen()

        if subWidget is not self.__tmpCurrentScreen:
            self.__widget = SingleApplicationScreenWidget(subWidget, useTopBar=True, useBottomBar=True)
            self.__tmpCurrentScreen = subWidget

        return self.__widget

    def on_set(self):
        """
            override of superclass method
        """

        self.__app.app().on_set()

    def on_unset(self):
        """
            override of superclass method
        """

        pass