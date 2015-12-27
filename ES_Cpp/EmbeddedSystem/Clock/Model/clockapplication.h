#ifndef EMBEDDEDSYSTEM_CLOCK_CLOCKAPPLICATION_H
#define EMBEDDEDSYSTEM_CLOCK_CLOCKAPPLICATION_H

#include "API/application.h"

namespace EmbeddedSystem{
    namespace Clock{
        namespace Model{

            class ClockApplication : public API::Application {
                public:
                    ClockApplication();
            };

        }
    }
}

#endif // EMBEDDEDSYSTEM_CLOCK_CLOCKAPPLICATION_H
