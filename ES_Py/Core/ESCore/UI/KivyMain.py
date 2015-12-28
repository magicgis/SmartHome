import kivy
kivy.require('1.0.6') # replace with your current kivy version !

from kivy.app import App
from kivy.app import Widget
from kivy.uix.boxlayout import BoxLayout
from kivy.uix.video import Video
from kivy.uix.button import Button
from kivy.core.image import Image
import ESApi.FileIO as FileIO
import ESCore.UI.MainWidget as MainWidget
import ESCore.Controller.LoadingScreenController as LoadingScreenController

class KivyMain(App):
    def build(self):
        MainWidget.instance.set_controller(LoadingScreenController.instance)
        return MainWidget.instance