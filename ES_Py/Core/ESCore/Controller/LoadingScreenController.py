from kivy.app import Widget

from ESCore.UI.LoadingScreenWidget import LoadingScreenWidget
from ESCore.Controller.Controller import Controller
from ESCore.Threads.BootThread import BootThread


class LoadingScreenController(Controller):
    widget = LoadingScreenWidget()  # type: LoadingScreenWidget
    thread = None  # type: BootThread

    def get_widget(self) -> Widget:
        return self.widget

    def on_set(self):
        self.thread = BootThread()
        self.thread.set_callback(self.__on_boot_finished)
        self.thread.start()
        return

    def on_unset(self):
        return

    def __on_boot_finished(self):
        print("BOOT FINISHED")

instance = LoadingScreenController()