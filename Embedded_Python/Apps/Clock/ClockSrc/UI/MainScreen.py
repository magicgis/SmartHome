from kivy.uix.boxlayout import BoxLayout
from kivy.uix.image import Image
from kivy.uix.label import Label
from kivy.uix.button import Button
from ESApi.AppScreen import AppScreen

import ClockSrc.TimeManager as TimeManager

class MainScreen(AppScreen, BoxLayout):
    __background = None  # type: Widget
    __time = None  # type: Label
    __date = None  # type: Label
    __timersButton = None  # type: Button


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

        self.__timersButton = Button()
        self.__timersButton.text = "Timers"

        TimeManager.instance.register_time_listener(self.__timemanager_timechanged)
        TimeManager.instance.register_date_listener(self.__timemanager_datechanged)

    def _on_resize(self):
        pass

    def update_time(self, hours: int, minutes: int):
        string = ""

        if hours < 10:
            string += "0"

        string += str(hours) + ":"

        if minutes < 10:
            string += "0"

        string += str(minutes)

        self.__time.text = string

    def update_date(self, weekday: str, day: int, month: int, year: int):
        self.__date.text = weekday + " " + str(day) + "." + str(month) + "." + str(year)

    def on_set(self):
        pass

    def on_unset(self):
        pass

    def __timemanager_timechanged(self, hours: int = None, minutes: int = None, seconds: int = None):
        self.update_time(hours, minutes)

    def __timemanager_datechanged(self, year: int = None, month: int = None, day: int = None, weekday: int = None):
        wd = ""

        if weekday == 0:
            wd = "Mon"
        elif weekday == 1:
            wd = "Tue"
        elif weekday == 2:
            wd = "Wed"
        elif weekday == 3:
            wd = "Thu"
        elif weekday == 4:
            wd = "Fri"
        elif weekday == 5:
            wd = "Sat"
        elif weekday == 6:
            wd = "Sun"

        self.update_date(wd, day, month, year)

    def get_topbar_buttons(self):
        return [self.__timersButton]