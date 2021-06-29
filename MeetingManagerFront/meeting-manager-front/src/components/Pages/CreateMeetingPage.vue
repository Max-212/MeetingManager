<script>
import MeetingForm from '../Forms/MeetingForm';
import api from '../../api/api'

export default {
    data()
    {
        return{
            errors: {}
        }
    },
    components:{
        MeetingForm
    },
    methods:
    {
        createMeeting(meeting)
        {

            api.meeting.Create(
            {
                From: meeting.from,
                Till: meeting.till,
                Description: meeting.description,
                Participants: meeting.participants
            })
            .then(() =>
            {
                this.$router.replace('/')   
            })
            .catch(error =>
            {
                this.errors = error.response.data.errors;
            })
        }
    }
}
</script>

<template>
    <MeetingForm :errors="errors" v-on:saveMeeting="createMeeting"></MeetingForm>
</template>