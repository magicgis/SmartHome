from kivy.uix.boxlayout import BoxLayout
from kivy.uix.image import Image
from ESApi.AppScreen import AppScreen


class MainScreen(AppScreen, BoxLayout):
    __testWidget = None  # type: Widget

    def __init__(self, **kwargs):
        super(MainScreen, self).__init__(**kwargs)
        self.__testWidget = Image()
        self.__testWidget.color = [0, 0, 0, 1]
        self.add_widget(self.__testWidget)

    def _on_resize(self):
        pass