#ifndef MAINWINDOWCONTROLLER_H
#define MAINWINDOWCONTROLLER_H

#include "mainwindow.h"

namespace EmbeddedSystem{
    namespace Core{
        namespace Controller{

            class MainWindowController
            {
                private:
                    static MainWindowController* s_instance;

                public:
                    static MainWindowController* get_instance();
                    static void provide_instance(MainWindow* p_window);

                private:
                    MainWindow* m_main_window;

                private:
                    MainWindowController(MainWindow* p_main_window);
            };

        }
    }
}

#endif // MAINWINDOWCONTROLLER_H
