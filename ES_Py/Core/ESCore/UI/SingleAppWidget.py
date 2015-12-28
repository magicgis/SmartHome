from kivy.app import Widget


class SingleAppWidget(Widget):
    def __init__(self, **kwargs):
        super(SingleAppWidget, self).__init__(**kwargs)

    def change_size(self, width: int, height: int):
        self.size[0] = width
        self.size[1] = height
        self._on_resize()

    def _on_resize(self):
        pass