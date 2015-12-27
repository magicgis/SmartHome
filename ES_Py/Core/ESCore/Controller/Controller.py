from kivy.app import Widget

class Controller:
    def get_widget(self) -> Widget:
        raise NotImplementedError()

    def on_set(self):
        raise NotImplementedError()

    def on_unset(self):
        raise NotImplementedError()