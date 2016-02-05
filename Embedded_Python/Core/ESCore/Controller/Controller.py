from kivy.app import Widget


class Controller:
    """
        A controller is part of the mvc pattern used for displaying graphics on screen
    """

    def get_widget(self) -> Widget:
        """
            Abstract method to be implemented by base class
        :return: The controllers widget to be displayed
        """

        raise NotImplementedError()

    def on_set(self):
        """
            Abstract method to be implemented by base class
            Gets called if the main widget sets and displays this controllers widget
        """

        pass

    def on_unset(self):
        """
            Abstract method to be implemented by base class
            Gets called if the main widget removes and stops displaying this controllers widget
        """

        pass