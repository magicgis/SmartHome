import kivy
kivy.require('1.0.6') # replace with your current kivy version !

from kivy.app import App
from kivy.app import Widget
from kivy.uix.button import Button
from kivy.uix.label import Label


class KivyMain(App):
    def build(self):
        main = MainWidget()
        main.set_widget(Button())
        return main

class MainWidget:
    activeWidget = None #type: Widget
    mainWidget = None #type: Widget

    def __init__(self):
        self.mainWidget = Widget()

    def set_widget(self, widget: Widget):
        if not self.activeWidget is None:
            self.mainWidget.remove_widget(self.activeWidget)

        self.mainWidget.add_widget(widget)
        self.activeWidget = widget
        self.activeWidget.width = self.mainWidget.width
        self.activeWidget.height = self.mainWidget.height