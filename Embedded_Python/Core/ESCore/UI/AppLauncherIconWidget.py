from kivy.uix.gridlayout import GridLayout
from kivy.uix.anchorlayout import AnchorLayout
from kivy.uix.image import Image as ImageWidget
from kivy.uix.label import Label
from kivy.core.image import Image

from ESCore.AppStarter import AppStarter

class AppLauncherIconWidget(GridLayout):
    """
        Displays icon and name of a single application
        Also handles input on the app icon
    """

    __imageParent = None  # type: AnchorLayout
    __imageWidget = None  # type: ImageWidget
    __nameWidget = None  # type: Label

    __starter = None  # type: AppStarter

    __paddingTmp = -1  # type: int
    __touched = False  # type: int

    def __init__(self, image: Image, name: str, starter: AppStarter, height: int, **kwargs):
        super(AppLauncherIconWidget, self).__init__(**kwargs)

        self.__starter = starter

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
        if self.__paddingTmp is -1:
            self.__imageParent.padding = [(size - (size * percentage)) / 2, (size - (size * percentage)) / 2, (size - (size * percentage)) / 2, (size - (size * percentage)) / 2]
        else:
            self.__paddingTmp = (size - (size * percentage)) / 2

    def on_touch_down(self, touch):
        if self.__imageWidget.collide_point(*touch.pos):
            self.__touched = True
            self.__paddingTmp = self.__imageParent.padding[0]
            self.__imageParent.padding = [self.__paddingTmp * 1 / 0.8, self.__paddingTmp * 1 / 0.8, self.__paddingTmp * 1 / 0.8, self.__paddingTmp * 1 / 0.8]
        else:
            self.__touched = False

    def on_touch_up(self, touch):
        if self.__imageParent.collide_point(*touch.pos):
            self.__starter.start_app()

        self.__imageParent.padding = [self.__paddingTmp, self.__paddingTmp, self.__paddingTmp, self.__paddingTmp]
        self.__paddingTmp = -1
        self.__touched = False