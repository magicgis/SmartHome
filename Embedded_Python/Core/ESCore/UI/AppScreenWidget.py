from kivy.uix.boxlayout import BoxLayout

from ESCore.UI.SingleApplicationScreenSubWidget import SingleApplicationScreenSubWidget
from ESApi.AppScreen import AppScreen


class AppScreenWidget(SingleApplicationScreenSubWidget, BoxLayout):
    """
        Displays a single screen of an active application
    """

    __appScreen = None  # type: AppScreen

    def __init__(self, screen: AppScreen, **kwargs):
        super(AppScreenWidget, self).__init__(**kwargs)
        self.__appScreen = screen
        self.add_widget(self.__appScreen)

    def _on_resize(self):
        self.__appScreen.change_size(self.width, self.height)