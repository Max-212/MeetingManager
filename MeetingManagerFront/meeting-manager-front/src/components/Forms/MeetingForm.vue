<script>
import axios from 'axios'
import UserDropdownItem from '../Content/UserDropdownItem';
import Participant from '../Content/Participant';

export default {
    data()
    {
        return{
            from: this.meeting.from,
            till: this.meeting.till,
            description: this.meeting.description,
            participants : [],
            users: [],
            errorMessage: '',
        } 
    },
    props:
    {
        meeting:
        {
            type: Object,
            default(){
                return {}
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
        if(this.meeting && this.meeting.participants)
        {
            this.participants = this.meeting.participants;
        }
        this.fetchUsers();
    },
    beforeUpdate()
    {
        this.generateErrorMessage();
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

        generateErrorMessage()
        {
            if(Object.keys(this.errors)[0])
            {
                this.errorMessage = this.errors[Object.keys(this.errors)[0]][0]
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
            if(this.from.length === 0 || this.till.length === 0)
            {
                this.errorMessage = "Wrong Format in From or Till field"
                return false;
            }
            return true;
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
            axios.get("https://localhost:44343/api/users/")
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
                    <input type="datetime-local" class="form-control w-100" v-model="from"/>
                </div>
                <div class="d-flex flex-column mb-4 w-100">
                    <span class="d-block">Till date</span>
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
                        <div v-if="users" class="dropdown-menu w-100" aria-labelledby="dropdownMenuButton1">
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

        <div v-if="errorMessage" className="form-group">
            <div className="alert alert-danger" role="alert">
                {{errorMessage}}
            </div>
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