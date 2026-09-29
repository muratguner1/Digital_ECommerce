import { createSlice } from "@reduxjs/toolkit";

const searchSlice = createSlice({
    name: "search",
    initialState: { 
        value: '',
    },
    reducers: {
        setSearchQuery: (state, action) => {
            state.value = action.payload;
        },
        clearSeacrhQuery: (state) => {
            state.value = '';
        },
        resetSearchQuery: (state) => {
            state.value = '';
        }
    }
})

export const {setSearchQuery, clearSeacrhQuery, resetSearchQuery} = searchSlice.actions
export default searchSlice.reducer