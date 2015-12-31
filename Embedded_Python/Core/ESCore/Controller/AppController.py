from kivy.app import Widget

from ESCore.Controller.Controller import Controller
from ESCore.CoreApplication import CoreApplication


class AppController(Controller):
    """
        Handles displaying apps and their corresponding screens
    """

    __app = None  # type: CoreApplication

    def __init__(self, app: CoreApplication):
        """
        :param app: The app this controller has to display
        """

        self.__app = app

    def get_widget(self) -> Widget:
        """
            override of superclass method
        """

        self.__app.app().get_current_screen()

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