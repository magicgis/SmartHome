from kivy.uix.boxlayout import BoxLayout
from kivy.uix.image import Image
from kivy.uix.label import Label
from kivy.uix.button import Button
from ESApi.AppScreen import AppScreen

import ClockSrc.TimeManager as TimeManager


class TimerScreen(AppScreen, BoxLayout):
    __background = None  # type: Widget

    def __init__(self, **kwargs):
        super(TimerScreen, self).__init__(**kwargs)
        self.__background = Image()
        self.__background.color = [1, 0, 1, 1]
        self.add_widget(self.__background)