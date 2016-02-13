from kivy.uix.boxlayout import BoxLayout
from kivy.uix.image import Image
from kivy.uix.label import Label
from ESApi.AppScreen import AppScreen


class MainScreen(AppScreen, BoxLayout):
    __background = None  # type: Widget
    __time = None  # type: Label
    __date = None  # type: Label


    def __init__(self, **kwargs):
        super(MainScreen, self).__init__(**kwargs)
        self.__background = Image()
        self.__background.color = [1, 1, 1, 1]
        self.add_widget(self.__background)

        self.__time = Label()
        self.__time.text = "12:00"
        self.__time.font_size = 200
        self.__time.pos = (337, 225)
        self.__time.color = [0, 0, 0, 1]
        self.__background.add_widget(self.__time)

        self.__date = Label()
        self.__date.text = "Sat 13.02.2016"
        self.__date.font_size = 50
        self.__date.pos = (337, 100)
        self.__date.color = [0, 0, 0, 1]
        self.__background.add_widget(self.__date)

    def _on_resize(self):
        pass

    def on_set(self):
        pass

    def on_unset(self):
        pass