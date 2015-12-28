from kivy.uix.gridlayout import GridLayout
from kivy.app import Widget
from kivy.uix.image import Image
from kivy.core.window import Window


class SingleApplicationScreenWidget(GridLayout):
    __childWidget = None  # type: Widget
    __bottomBar = None  # type: Widget
    __topBar = None  # type: Widget

    __useBottomBar = True  # type: Widget
    __useTopBar = True  # type: Widget

    __bottomBarHeight = 50  # type: int
    __topBarHeight = 100  # type: int

    def __init__(self, child: Widget, useTopBar: bool = True, useBottomBar: bool = True, **kwargs):
        super(SingleApplicationScreenWidget, self).__init__(**kwargs)
        self.cols = 1
        self.__useBottomBar = useBottomBar
        self.__useTopBar = useTopBar
        self.__childWidget = child
        self.__create_bars()
        self.__update_layout()

    def __create_bars(self):
        self.__bottomBar = Image()
        self.__bottomBar.color = [0, 0, 0, 0]
        self.__bottomBar.allow_stretch = True
        self.__topBar = Image()
        self.__topBar.color = [0, 0, 0, 0]
        self.__bottomBar.allow_stretch = True
        self.col_default_width = Window.size[0]
        self.col_force_default = True
        self.row_force_default = True

    def __update_layout(self):
        for child in self.children:
            self.remove_widget(child)

        if self.__useBottomBar and self.__useTopBar:
            self.rows = 3
            self.rows_minimum[0] = self.__topBarHeight
            self.rows_minimum[1] = Window.size[1] - self.__bottomBarHeight - self.__topBarHeight
            self.rows_minimum[2] = self.__bottomBarHeight
            self.add_widget(self.__topBar)
            self.add_widget(self.__childWidget)
            self.add_widget(self.__bottomBar)
        elif self.__useBottomBar and not self.__useTopBar:
            self.rows = 2
            self.rows_minimum[0] = Window.size[1] - self.__bottomBarHeight
            self.rows_minimum[1] = self.__bottomBarHeight
            self.add_widget(self.__childWidget)
            self.add_widget(self.__bottomBar)
        elif self.__useTopBar and not self.__useBottomBar:
            self.rows = 2
            self.rows_minimum[0] = self.__topBarHeight
            self.rows_minimum[1] = Window.size[1] - self.__topBarHeight
            self.add_widget(self.__topBar)
            self.add_widget(self.__childWidget)
        else:
            self.rows = 1
            self.rows_minimum[0] = Window.size[1]
            self.add_widget(self.__childWidget)

    def toggle_bars(self, topBar: bool = True, bottomBar: bool = True):
        updateLayout = (topBar is not self.__useTopBar) or (bottomBar is not self.__useBottomBar)

        self.__useTopBar = topBar
        self.__useBottomBar = bottomBar

        if updateLayout:
            self.__update_layout()