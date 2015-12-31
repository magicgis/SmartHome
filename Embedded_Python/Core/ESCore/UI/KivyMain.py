import kivy
kivy.require('1.0.6') # replace with your current kivy version !

from kivy.app import App
import ESCore.UI.MainWidget as MainWidget
import ESCore.Controller.LoadingScreenController as LoadingScreenController


class KivyMain(App):
    """
        Main class of the kivy gui system
    """

    def build(self):
        MainWidget.instance.set_controller(LoadingScreenController.instance)
        return MainWidget.instance
