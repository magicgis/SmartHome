from kivy.uix.gridlayout import GridLayout
from kivy.uix.anchorlayout import AnchorLayout
from kivy.uix.image import Image as ImageWidget
from kivy.uix.label import Label
from kivy.core.image import Image
from kivy.graphics import Color, Rectangle


class AppIconWidget(GridLayout):
    __imageParent = None  # type: AnchorLayout
    __imageWidget = None  # type: ImageWidget
    __nameWidget = None  # type: Label

    def __init__(self, image: Image, name: str, height: int, **kwargs):
        super(AppIconWidget, self).__init__(**kwargs)
        self.cols = 1
        self.rows = 2

        self.rows_minimum[0] = height * 0.8
        self.rows_minimum[1] = height - self.rows_minimum[0]

        self.row_force_default = True

        self.__imageParent = AnchorLayout()
        self.__imageParent.anchor_x = 'center'
        self.__imageParent.anchor_y = 'center'

        self.__imageWidget = ImageWidget()
        self.__imageWidget.allow_stretch = True
        self.__imageWidget.keep_ratio = True
        self.__imageWidget.texture = image.texture

        self.__nameWidget = Label()
        self.__nameWidget.text = name
        self.__nameWidget.color = [0, 0, 0, 1]

        self.__imageParent.add_widget(self.__imageWidget)
        self.add_widget(self.__imageParent)
        self.add_widget(self.__nameWidget)

    def set_image_size(self, size: int, percentage: float):
        self.__imageParent.padding = [(size - (size * percentage)) / 2, (size - (size * percentage)) / 2, (size - (size * percentage)) / 2, (size - (size * percentage)) / 2]