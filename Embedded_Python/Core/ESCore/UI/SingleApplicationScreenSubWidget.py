from kivy.app import Widget


class SingleApplicationScreenSubWidget(Widget):
    def __init__(self, **kwargs):
        super(SingleApplicationScreenSubWidget, self).__init__(**kwargs)

    def change_size(self, width: int, height: int):
        self.size[0] = width
        self.size[1] = height
        self._on_resize()

    def _on_resize(self):
        pass