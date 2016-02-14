from ESCore.UI.SingleApplicationScreenWidget import SingleApplicationScreenSubWidget

class ScreenStack:
    __stack = []

    def push(self, screen: SingleApplicationScreenSubWidget):
        self.__stack.append(screen)

    def pop(self) -> SingleApplicationScreenSubWidget:
        if len(self.__stack) == 0:
            return None
        elif len(self.__stack) == 1:
            return self.get_first_element()
        else:
            return self.__stack.pop()

    def count(self):
        return len(self.__stack)

    def get_first_element(self) -> SingleApplicationScreenSubWidget:
        return self.__stack[0]