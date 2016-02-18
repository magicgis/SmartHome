from kivy.uix.boxlayout import BoxLayout
from kivy.uix.gridlayout import GridLayout
from kivy.uix.image import Image
from kivy.uix.button import Button
from kivy.core.window import Window

from ESApi.AppScreen import AppScreen


class LightSelection(AppScreen, BoxLayout):
    __background = None  # type: Image
    __width = None  # type: int
    __height = None  # type: int

    __lights = []

    def __init__(self, **kwargs):
        super(LightSelection, self).__init__(**kwargs)
        self.__background = Image(color=[1, 1, 1, 1])
        self.add_widget(self.__background)

    def clear_lights(self):
        for child in self.__background.children:
            self.__background.remove_widget(child)

        for child in self.__background.children:
            self.__background.remove_widget(child)

    def __refresh_lights(self):
        self.clear_lights()

        for index in range(0, len(self.__lights)):
            btn = self.__lights[index]  # type: Button
            btn.width = Window.size[0]
            btn.height = 100
            btn.pos = (0, Window.size[1] - 55 - ((index + 1) * 100))
            self.__background.add_widget(btn)

    def add_light(self, name: str, callback: callable):
        btn = Button()
        btn.text = name
        btn.bind(on_press=callback)
        self.__lights.append(btn)
        self.__refresh_lights()