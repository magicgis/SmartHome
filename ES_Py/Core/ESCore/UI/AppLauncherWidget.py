from kivy.app import Widget
from kivy.core.window import Window
from kivy.uix.boxlayout import BoxLayout
from kivy.graphics import Color, Rectangle
from kivy.core.image import Image

from ESCore.UI.SingleAppWidget import SingleAppWidget

import ESCore.CoreFileIO as FileIO


class AppLauncherWidget(SingleAppWidget, BoxLayout):
    def __init__(self, **kwargs):
        super(AppLauncherWidget, self).__init__(**kwargs)
        with self.canvas.before:
            Color(1, 1, 1, 1)
            self.rect = Rectangle(size=self.size, pos=self.pos, source=FileIO.sysdata_directory() + "/launcher/background.png")

        self.bind(size=self._update_rect, pos=self._update_rect)

    def _update_rect(self, instance, value):
        self.rect.pos = instance.pos
        self.rect.size = instance.size

    def _on_resize(self):
        pass#self.__backgroundImage.size = self.size