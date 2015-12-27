#include "mainwindow.h"
#include "ui_mainwindow.h"

#include "Core/Controller/mainwindowcontroller.h"

using namespace EmbeddedSystem::Core::Controller;

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    MainWindowController::provide_instance(this);

    ui->single_app_widget->setEnabled(false);
    ui->single_app_widget->setVisible(false);

    ui->loading_screen_widget->setEnabled(false);
    ui->loading_screen_widget->setVisible(false);

    ui->single_app_widget->setEnabled(true);
    ui->single_app_widget->setVisible(true);
}

MainWindow::~MainWindow()
{
    delete ui;
}
