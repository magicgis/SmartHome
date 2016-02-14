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
        self.use_top_bar = screen.use_top_bar
        self.use_bottom_bar = screen.use_bottom_bar
        self.add_widget(self.__appScreen)

    def _on_resize(self):
        self.__appScreen.change_size(self.width, self.height)

    def get_topbar_buttons(self):
        return self.__appScreen.get_topbar_buttons()