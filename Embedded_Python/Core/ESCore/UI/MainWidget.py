from kivy.uix.boxlayout import BoxLayout
from kivy.app import Widget
from ESCore.Controller.Controller import Controller


class MainWidget(BoxLayout):
    activeController = None  # type: Controller

    def __init__(self, **kwargs):
        super(MainWidget, self).__init__(**kwargs)

    def set_controller(self, controller: Controller):
        if not self.activeController is None:
            self.activeController.on_unset()
            self.remove_widget(self.activeController.get_widget())

        widget = controller.get_widget()
        self.add_widget(widget)
        self.activeController = controller
        self.activeController.get_widget().width = self.width
        self.activeController.get_widget().height = self.height
        self.activeController.on_set()

instance = MainWidget()