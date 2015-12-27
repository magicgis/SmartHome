#ifndef EMBEDDEDSYSTEM_API_APPLICATION_H
#define EMBEDDEDSYSTEM_API_APPLICATION_H

#include <QImage>
#include <string>

namespace EmbeddedSystem{
    namespace API{

        class Application
        {
            protected:
                QImage m_image;
                std::string m_name;

            public:
                Application(QImage p_image, std::string p_name);

                QImage get_image();
                std::string get_name();
        };

    }
}

#endif // EMBEDDEDSYSTEM_API_APPLICATION_H
