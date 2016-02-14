import ESCore.ApplicationManager as ApplicationManager
from ESCore.CoreApplication import CoreApplication

class AppStarter:
    __app = None  # type: CoreApplication

    def __init__(self, app: CoreApplication):
        self.__app = app

    def start_app(self):
        ApplicationManager.instance.start_app(self.__app)