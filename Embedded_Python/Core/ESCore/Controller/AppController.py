from kivy.app import Widget

from ESCore.Controller.Controller import Controller
from ESCore.CoreApplication import CoreApplication
from ESCore.UI.SingleApplicationScreenWidget import SingleApplicationScreenWidget
from ESCore.UI.SingleApplicationScreenSubWidget import SingleApplicationScreenSubWidget

import ESCore.UI.MainWidget as MainWidget

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
        self.__app.app().provide_refresh_callback(self.update_widget)

    def get_widget(self) -> Widget:
        """
            override of superclass method
        """

        subWidget = self.__app.app().get_current_screen()

        if subWidget is not self.__tmpCurrentScreen:
            self.__widget = SingleApplicationScreenWidget(subWidget)
            self.__tmpCurrentScreen = subWidget

        return self.__widget

    def update_widget(self):
        MainWidget.instance.set_controller(self)

    def on_set(self):
        """
            override of superclass method
        """

        self.__app.app().on_set()
        self.__app.app().get_current_screen().on_set()

    def on_unset(self):
        """
            override of superclass method
        """

        self.__app.app().get_current_screen().on_unset()
        self.__app.app().on_unset()