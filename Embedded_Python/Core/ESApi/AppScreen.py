from kivy.app import Widget


class AppScreen(Widget):
    """
        A app screen is a single screen displayed by an application
    """

    use_bottom_bar = True
    use_top_bar = True

    def __init(self, **kwargs):
        """
            Initialize the base Widget
        """

        super(AppScreen, self).__init__(**kwargs)

    def change_size(self, width: int, height: int):
        """
            Do not call this. This is called by the core system if the apps size has changed
            This can happen e.g. if the top and bottom bars get toggled

        :param width: The new applications width
        :param height: The new applications height
        """

        self.width = width
        self.height = height
        self._on_resize()

    def _on_resize(self):
        """
            Gets called when the size changes. The app screen can handle this by repositioning everything accordingly
        """

        pass

    def on_set(self):
        pass

    def on_unset(self):
        pass