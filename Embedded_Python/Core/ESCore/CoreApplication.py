from kivy.core.image import Image
import ESApi.Application as Application

class CoreApplication:
    __name = None  # type: str
    __icon = None  # type: Image
    __app = None  # type: Application

    def __init__(self, name: str, icon: Image, app: Application):
        self.__name = name
        self.__icon = icon
        self.__app = app

    def name(self) -> str:
        return self.__name

    def icon(self) -> Image:
        return self.__icon

    def app(self) -> Application:
        return self.__app
