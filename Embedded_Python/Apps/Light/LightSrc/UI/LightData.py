from kivy.uix.boxlayout import BoxLayout
from kivy.uix.gridlayout import GridLayout
from kivy.uix.image import Image
from kivy.core.image import Image as CoreImage
from kivy.uix.button import Button
from kivy.core.window import Window
from kivy.graphics import Color

from ESApi.AppScreen import AppScreen

import ESApi.FileIO

class LightData(AppScreen, BoxLayout):
    __background = None  # type: Image

    __colorPalette = None  # type: Image
    __brightnessBar = None  # type: Image

    __hue = 0  # type: int
    __sat = 0  # type: int
    __bri = 0  # type: int

    __callback = None  # type: callable

    def __init__(self, dataChangedCallback: callable, hue: int = 0, sat: int = 255, bri: int = 255, **kwargs):
        super(LightData, self).__init__(**kwargs)

        self.__callback = dataChangedCallback

        self.__background = Image(color=[1, 1, 1, 1])
        self.add_widget(self.__background)

        coreColorPalette = CoreImage.load(ESApi.FileIO.apps_directory() + "/Light/Media/HSBPalette.png")
        self.__colorPalette = Image()
        self.__colorPalette.texture = coreColorPalette.texture
        self.__colorPalette.allow_stretch = True
        self.__colorPalette.keep_ratio = False
        self.__background.add_widget(self.__colorPalette)

        coreBrightnessBar = CoreImage.load(ESApi.FileIO.apps_directory() + "/Light/Media/BrightnessBar.png");
        self.__brightnessBar = Image()
        self.__brightnessBar.texture = coreBrightnessBar.texture
        self.__brightnessBar.allow_stretch = True
        self.__brightnessBar.keep_ratio = False
        self.__background.add_widget(self.__brightnessBar)

        self.__hue = hue
        self.__sat = sat
        self.__bri = bri
        self.__debug_color()

    def update_data(self, hue: int = 0, sat: int = 255, bri: int = 255):
        self.__hue = hue
        self.__sat = sat
        self.__bri = bri
        self.__debug_color()

    def on_touch_down(self, touch):
        self.__touch_input(touch)

    def on_touch_move(self, touch):
        self.__touch_input(touch)

    def _on_resize(self):
        size = self.size[1]

        self.__brightnessBar.size = (100, size)
        self.__brightnessBar.pos = (self.size[0] - 100, 50)

        self.__colorPalette.size = (size, size)
        self.__colorPalette.pos = (self.__brightnessBar.pos[0] - size, 50)

    def __touch_input(self, touch):
        if self.__colorPalette.collide_point(touch.pos[0], touch.pos[1]): # Color Palette Input
            frameX = self.__colorPalette.pos[0]
            frameY = self.__colorPalette.pos[1]
            frameW = self.__colorPalette.size[0]
            frameH = self.__colorPalette.size[1]
            touchX = touch.pos[0]
            touchY = touch.pos[1]

            relativeX = float(touchX - frameX) / float(frameW)
            relativeY = float(touchY - frameY) / float(frameH)

            self.__sat = int(relativeX * 255)
            self.__hue = int(65535 - (relativeY * 65535))

            self.__debug_color(True)

        if self.__brightnessBar.collide_point(touch.pos[0], touch.pos[1]): # Brightness Bar Input
            relative = float(touch.pos[1] - self.__brightnessBar.pos[1]) / float(self.__brightnessBar.size[1])
            self.__bri = int(relative * 255)
            self.__debug_color(True)

    def __debug_color(self, useCallback: bool = False):
            self.__background.color = Color(float(self.__hue) / float(65535), float(self.__sat) / float(255), float(self.__bri) / float(255), mode="hsv").rgba

            if useCallback:
                self.__callback(hue=self.__hue, sat=self.__sat, bri=self.__bri)