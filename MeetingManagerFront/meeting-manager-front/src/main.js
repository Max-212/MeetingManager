import { createApp } from 'vue'
import App from './App.vue'
import { createWebHistory, createRouter } from "vue-router";

import HomePage from "./components/Pages/HomePage";
import CreateUserPage from "./components/Pages/CreateUserPage";
import MeetingsList from "./components/Content/MeetingsList";
import UsersList from "./components/Content/UsersList";
import UpdateUserPage from './components/Pages/UpdateUserPage';
import CreateMeetingPage from './components/Pages/CreateMeetingPage'
import UpdateMeetingPage from './components/Pages/UpdateMeetingPage'


const routes =
    [
        {
            path: "/", component: HomePage,
            children: [
                {path: '', component: MeetingsList},
                {path: 'users', component: UsersList}
            ]
        },
        {path: "/createUser", component: CreateUserPage},
        {path: "/updateUser/:id", component: UpdateUserPage},
        {path: "/createMeeting", component: CreateMeetingPage},
        {path: "/updateMeeting/:id", component: UpdateMeetingPage}
    ];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

createApp(App).use(router).mount('#app')
