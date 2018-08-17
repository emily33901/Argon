#pragma once

#include "testbed.hh"

class ListenerRegister {
public:
    static ListenerRegister *head;
    ListenerRegister *       next;

    steam::CallbackListener listener;
    int                     id;

    ListenerRegister(int id, steam::CallbackListener l) : id(id), listener(l) {
        next = head;
        head = this;
    }

    static void register_all() {
        for (auto l = head;
             l != nullptr;
             l = l->next) {
            steam::add_listener(l->id, l->listener);
        }
    }
};
