from kivy.uix.gridlayout import GridLayout
from kivy.app import Widget
from kivy.uix.image import Image
from kivy.core.window import Window
from kivy.uix.button import Button
from ESCore.UI.SingleApplicationScreenSubWidget import SingleApplicationScreenSubWidget
from ESCore.UI.ScreenStack import ScreenStack

class BottomBar(Widget):
    def __init__(self, height: int, backCallback: callable, homeCallback: callable, **kwargs):
        super(BottomBar, self).__init__(**kwargs)

        bottomBack = Image()
        bottomBack.color = [0, 0, 0, 1]
        bottomBack.width = Window.size[0]
        bottomBack.height = height
        self.add_widget(bottomBack)

        btnGrid = GridLayout()
        btnGrid.cols = 5
        btnGrid.rows = 1
        btnGrid.width = Window.size[0]
        btnGrid.height = height
        self.add_widget(btnGrid)

        btnBack = Button(text="Back")
        btnBack.bind(on_press=lambda instance: backCallback())
        btnGrid.add_widget(btnBack, 0)

        btnHome = Button(text="Home")
        btnHome.bind(on_press=lambda instance: homeCallback)
        btnGrid.add_widget(btnHome, 1)

class TopBar(Widget):
    __grid = None  # type: GridLayout

    def __init__(self, height: int, **kwargs):
        super(TopBar, self).__init__(**kwargs)
        topBack = Image()
        topBack.color = [0, 0, 0, 1]
        topBack.width = Window.size[0]
        topBack.height = height
        self.add_widget(topBack)

        self.__grid = GridLayout()
        self.__grid.cols = 10
        self.__grid.rows = 1
        self.__grid.width = Window.size[0]
        self.__grid.height = height
        self.add_widget(self.__grid)

    def clear_buttons(self):
        for child in self.__grid.children:
            self.__grid.remove_widget(child)

    def add_buttons(self, buttons):
        for index in range(0, len(buttons)):
            btn = buttons[index]  # type: Button
            self.__grid.add_widget(btn, self.__grid.cols - 1 - index)

class SingleApplicationScreenWidget(GridLayout):
    """
        Displays a single screen with the possibility to toggle a top and a bottom bar
    """

    __childWidget = None  # type: SingleApplicationScreenSubWidget
    __bottomBar = None  # type: Widget
    __topBar = None  # type: Widget

    __useBottomBar = True  # type: Widget
    __useTopBar = True  # type: Widget

    __bottomBarHeight = 50  # type: int
    __topBarHeight = 50  # type: int

    __stack = None  # type: ScreenStack

    def __init__(self, child: SingleApplicationScreenSubWidget, **kwargs):
        super(SingleApplicationScreenWidget, self).__init__(**kwargs)
        self.cols = 1
        self.__stack = ScreenStack()
        self.__stack.push(child)
        self.__useBottomBar = child.use_bottom_bar
        self.__useTopBar = child.use_top_bar
        self.__childWidget = child
        self.__create_bars()
        self.__update_layout()

    def __create_bars(self):
        self.__bottomBar = BottomBar(self.__bottomBarHeight, lambda: self.get_screen_back(), lambda: self.set_widget(self.__stack.get_first_element()))
        self.__topBar = TopBar(self.__topBarHeight)

        self.col_default_width = Window.size[0]
        self.col_force_default = True
        self.row_force_default = True

    def __update_layout(self):
        if self.__childWidget is not None:
            if self.__childWidget.parent is not None:
                self.__childWidget.parent.remove_widget(self.__childWidget)

        for child in self.children:
            self.remove_widget(child)

        for child in self.children:
            self.remove_widget(child)

        self.rows_minimum = []

        if self.__useTopBar:
            self.__topBar.clear_buttons()
            self.__topBar.add_buttons(self.__childWidget.get_topbar_buttons())

        if self.__useBottomBar and self.__useTopBar:
            self.rows = 3
            self.rows_minimum[0] = self.__topBarHeight
            self.rows_minimum[1] = Window.size[1] - self.__bottomBarHeight - self.__topBarHeight
            self.rows_minimum[2] = self.__bottomBarHeight
            self.add_widget(self.__topBar)
            self.add_widget(self.__childWidget)
            self.add_widget(self.__bottomBar)
            self.__childWidget.change_size(Window.size[0], self.rows_minimum[1])
        elif self.__useBottomBar and not self.__useTopBar:
            self.rows = 2
            self.rows_minimum[0] = Window.size[1] - self.__bottomBarHeight
            self.rows_minimum[1] = self.__bottomBarHeight
            self.add_widget(self.__childWidget)
            self.add_widget(self.__bottomBar)
            self.__childWidget.change_size(Window.size[0], self.rows_minimum[0])
        elif self.__useTopBar and not self.__useBottomBar:
            self.rows = 2
            self.rows_minimum[0] = self.__topBarHeight
            self.rows_minimum[1] = Window.size[1] - self.__topBarHeight
            self.add_widget(self.__topBar)
            self.add_widget(self.__childWidget)
            self.__childWidget.change_size(Window.size[0], self.rows_minimum[1])
        else:
            self.rows = 1
            self.rows_minimum[0] = Window.size[1]
            self.add_widget(self.__childWidget)

    def set_widget(self, widget: SingleApplicationScreenSubWidget):
        if self.__childWidget == widget:
            return

        self.__childWidget = widget
        self.__useBottomBar = self.__childWidget.use_bottom_bar
        self.__useTopBar = self.__childWidget.use_top_bar
        self.__stack.push(widget)
        self.__update_layout()

    def get_screen_back(self):
        screen = self.__stack.pop()

        if screen == None:
            return

        self.set_widget(screen)

    def toggle_bars(self, topBar: bool = True, bottomBar: bool = True):
        updateLayout = (topBar is not self.__useTopBar) or (bottomBar is not self.__useBottomBar)

        self.__useTopBar = topBar
        self.__useBottomBar = bottomBar

        if updateLayout:
            self.__update_layout()