from kivy.uix.boxlayout import BoxLayout
from kivy.uix.label import Label
from ESApi.AppScreen import AppScreen


class MainScreen(AppScreen, BoxLayout):
    __testWidget = None  # type: Widget

    def __init__(self, **kwargs):
        super(MainScreen, self).__init__(**kwargs)
        self.__testWidget = Label()
        self.__testWidget.text = "Hello WOrld"
        self.add_widget(self.__testWidget)

    def _on_resize(self):
        pass