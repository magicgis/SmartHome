from kivy.uix.gridlayout import GridLayout
from kivy.graphics import Color, Rectangle

from ESCore.UI.SingleApplicationScreenSubWidget import SingleApplicationScreenSubWidget
from ESCore.UI.AppLauncherIconWidget import AppLauncherIconWidget
from ESCore.AppStarter import AppStarter

import ESCore.ApplicationManager as ApplicationManager
import ESCore.CoreFileIO as FileIO


class AppLauncherWidget(SingleApplicationScreenSubWidget, GridLayout):
    """
        Displays a background and all installed apps so the user can select and start one
    """

    def __init__(self, **kwargs):
        super(AppLauncherWidget, self).__init__(**kwargs)

        self.cols = 4
        self.rows = 2

        self.col_force_default = True
        self.row_force_default = True

        with self.canvas.before:
            Color(1, 1, 1, 1)
            self.rect = Rectangle(size=self.size, pos=self.pos, source=FileIO.sysdata_directory() + "/launcher/background.png")
        self.bind(size=self._update_rect, pos=self._update_rect)

    def _update_rect(self, instance, value):
        self.rect.pos = instance.pos
        self.rect.size = instance.size

    def _on_resize(self):
        self.col_default_width = self.size[0] / self.cols
        self.row_default_height = self.size[1] / self.rows

        for widget in self.children:
            self.remove_widget(widget)

        for index in range(0, ApplicationManager.instance.application_count()):
            app = ApplicationManager.instance.application_at(index)
            widget = AppLauncherIconWidget(app.icon(), app.name(), AppStarter(app), self.row_default_height)
            widget.set_image_size(self.row_default_height, 0.7)
            self.add_widget(widget)