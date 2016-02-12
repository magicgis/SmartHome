import os
import sys
from kivy.core.image import Image

from ESCore.CoreApplication import CoreApplication
from ESApi.Application import Application
import ESCore.UI.MainWidget as MainWidget
from ESCore.Controller.AppController import AppController


class ApplicationManager:
    """
        Handles all loaded applications
    """

    __applications = []  # type: List[CoreApplication]

    def application_count(self) -> int:
        """
        :return: Number of applications loaded
        """

        return len(self.__applications)

    def application_at(self, index: int) -> CoreApplication:
        """
            Provides a application at a specific index
        :param index: The applications index
        :return: Application at the specified index
        """

        if index < 0 or index >= self.application_count():
            raise IndexError()

        return self.__applications[index]

    def load_applications(self, apps_dir: str):
        """
            Loads all apps from a source dir
        :param apps_dir: The apps source dir
        """

        for top, dirs, files in os.walk(apps_dir):
            if top != apps_dir:
                continue

            for app in dirs:
               icon = self.__load_icon(apps_dir + "/" + app)
               iApp = self.__load_app(apps_dir + "/" + app, app)
               cApp = CoreApplication(app, icon, iApp)
               self.__applications.append(cApp)

    def start_app(self, app: CoreApplication):
        """
            Starts a app
        :param app: App to start
        """

        MainWidget.instance.set_controller(AppController(app))

    def __load_app(self, app_dir: str, app_name: str) -> Application:
        sys.path.append(app_dir + "/")
        module = __import__(app_name + "Src." + app_name + "App")
        type = getattr(module, app_name + "App")
        type = getattr(type, app_name + "App")
        instance = type()
        return instance


    def __load_icon(self, app_dir: str) -> Image:
        iconPath = app_dir + "/Icon.png"
        icon = Image.load(iconPath)
        return icon

instance = ApplicationManager() #type: ApplicationManager