from kivy.app import Widget


class AppScreen(Widget):
    def __init(self, **kwargs):
        super(AppScreen, self).__init__(**kwargs)

    def change_size(self, width: int, height: int):
        self.width = width
        self.height = height
        self._on_resize()

    def _on_resize(self):
        pass