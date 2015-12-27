#include "mainwindowcontroller.h"

#include <iostream>
#include "API/coresystem.h"

using namespace EmbeddedSystem::Core::Controller;
using namespace EmbeddedSystem::API;

MainWindowController* MainWindowController::s_instance;

void MainWindowController::provide_instance(MainWindow* p_window){
    if(s_instance != NULL)
        return;

    s_instance = new MainWindowController(p_window);
}

MainWindowController::MainWindowController(MainWindow* p_main_window){
    m_main_window = p_main_window;
    CoreSystem::init();
}
