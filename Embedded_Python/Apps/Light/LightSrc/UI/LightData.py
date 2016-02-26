from kivy.uix.boxlayout import BoxLayout
from kivy.uix.gridlayout import GridLayout
from kivy.uix.image import Image
from kivy.uix.button import Button
from kivy.core.window import Window

from ESApi.AppScreen import AppScreen


class LightData(AppScreen, BoxLayout):
    __background = None  # type: Image

    __btnBack = None  # type: Button

    def __init__(self, backCallback: callable, **kwargs):
        super(LightData, self).__init__(**kwargs)
        self.__background = Image(color=[0.3, 0.7, 0.5, 1])
        self.add_widget(self.__background)

        self.__btnBack = Button()
        self.__btnBack.text = "Back"
        self.__btnBack.bind(on_press=lambda instance: backCallback)

    def get_topbar_buttons(self):
        return [self.__btnBack]

    def on_touch_down(self, touch):
        self.__touch_input(touch)

    def on_touch_move(self, touch):
        self.__touch_input(touch)

    def __touch_input(self, touch):
        print("Touched: " + str(touch.pos))