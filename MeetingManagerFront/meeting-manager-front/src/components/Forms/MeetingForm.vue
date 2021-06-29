<script>
import api from '../../api/api'
import UserDropdownItem from '../Content/UserDropdownItem';
import Participant from '../Content/Participant';

export default {

    data()
    {
        return{
            from: '',
            till: '',
            description: '',
            participants : [],
            users: [],
            validationsMessages: {}
        }
    },

    props:
    {
        meeting:
        {
            type: Object,
            default(){
                return null
            } 
        },
        errors:{
            type: Object,
            default(){
                return {}
            }
        }
    },

    components:
    {
        UserDropdownItem,
        Participant
    },

    mounted()
    {
        if(this.meeting)
        {
            this.participants = this.meeting.participants;
            this.from = this.meeting.from;
            this.till = this.meeting.till;
            this.description = this.meeting.description;
        }
        this.fetchUsers();
    },

    methods:
    {
        addParticipant(user)
        {
            if(!this.participants.find(participant => participant.id === user.id))
            {
                this.participants.push(user);
            }
        },

        removeParticipant(user)
        {
            let index = this.participants.indexOf(user);
            if (index > -1) {
                this.participants.splice(index, 1);
            }
        },

        validateFields()
        {
            this.validationsMessages = {}
            let result = true;
            if(this.from.length === 0)
            {
                this.validationsMessages.From = "This field can not be empty"
                result = false;
            }
            if(this.till.length === 0)
            {
                this.validationsMessages.Till = "This field can not be empty"
                result = false;
            }
            return result;
        },

        saveMeeting()
        {
            if(!this.validateFields())
            {
                return;
            }
            let participants = this.participants.map((participant) =>
            {
                return participant.id
            });
            this.$emit('saveMeeting',
            {
                from : this.from,
                till : this.till,
                description : this.description,
                participants : participants
            });
        },

        fetchUsers()
        {
            api.user.GetAll()
            .then(response =>
            {
                this.users = response.data;
            })
        }
    }
}

</script>


<template>

    <div class="d-flex flex-column justify-content-center align-items-center w-100">
        <div class="fw-bold fs-1 mb-4">Meeting</div>
        <div class="shadow d-flex justify-content-around w-100 flex-wrap meeting-form p-5">

            <form class="d-flex flex-column align-items-center flex-fill p-3">
                <div class="d-flex flex-column mb-4 w-100">
                    <span class="d-block">From date</span>
                    <span class="d-block text-danger" v-if="errors.From">{{errors.From[0]}}</span>
                    <span class="d-block text-danger" v-if="validationsMessages.From">{{validationsMessages.From}}</span>
                    <input type="datetime-local" class="form-control w-100" v-model="from"/>
                </div>
                <div class="d-flex flex-column mb-4 w-100">
                    <span class="d-block">Till date</span>
                    <span class="d-block text-danger" v-if="errors.Till">{{errors.Till[0]}}</span>
                    <span class="d-block text-danger" v-if="validationsMessages.Till">{{validationsMessages.Till}}</span>
                    <input type="datetime-local" class="form-control w-100" v-model="till"/>
                </div>
                <div class="form-group mb-4 w-100">
                    <textarea type="text" class="form-control" placeholder='Description' v-model="description"/>
                </div>
            </form>

            <div class="d-flex flex-column align-items-center justify-content-between p-3 w-100 meeting-form__participants">
                <div class="fw-bold fs-1">Participants</div>
                <div class="d-flex flex-column align-items-center">
                    <Participant v-for="participant in participants" :key="participant.id" :participant="participant" v-on:removeParticipant="removeParticipant"></Participant>
                </div>

                <div class="d-flex flex-column align-items-center w-100">
                    <div class="dropdown w-100">
                        <button class="btn btn-lg btn-secondary dropdown-toggle w-100" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">
                            Add Participant
                        </button>
                        <div v-if="users" class="dropdown-menu w-100 overflow-scroll" aria-labelledby="dropdownMenuButton1">
                            <UserDropdownItem @click="addParticipant(user)" v-for="user in users" :key="user.id" :user="user"></UserDropdownItem>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="form-outline mb-4 p-3">
            <input class="btn btn-success" type='submit' value='Save'
            @click="saveMeeting"/>
        </div>

    </div>

</template>

<style>
    .meeting-form
    {
        max-width: 1300px;
        background-color: rgb(255, 255, 255);
    }

    .meeting-form__participants
    {
        max-width: 500px;
    }
</style>