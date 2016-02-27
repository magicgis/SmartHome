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

    __btnBack = None  # type: Button

    __colorPalette = None  # type: Image
    __brightnessBar = None  # type: Image

    def __init__(self, backCallback: callable, **kwargs):
        super(LightData, self).__init__(**kwargs)
        self.__background = Image(color=[1, 1, 1, 1])
        self.add_widget(self.__background)

        self.__btnBack = Button()
        self.__btnBack.text = "Back"
        self.__btnBack.bind(on_press=lambda instance: backCallback)

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

    def get_topbar_buttons(self):
        return [self.__btnBack]

    def on_touch_down(self, touch):
        self.__touch_input(touch)

    def on_touch_move(self, touch):
        self.__touch_input(touch)

    def _on_resize(self):
        size = self.size[1]
        self.__colorPalette.size = (size, size)
        self.__colorPalette.pos = (self.size[0] - size, 50)

        self.__brightnessBar.size = (50, size)
        self.__brightnessBar.pos = (self.__colorPalette.pos[0], 50)

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

            sat = int(relativeX * 255)
            hue = int(65535 - (relativeY * 65535))

            self.__background.color = Color(float(hue) / float(65535), relativeX, 1, mode="hsv").rgba