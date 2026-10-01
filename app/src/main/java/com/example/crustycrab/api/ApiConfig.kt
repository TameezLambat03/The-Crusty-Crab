package com.example.crustycrab.api

object ApiConfig {
    /**
     * Base URL for the deployed CrustyAPI backend.
     *
     * The trailing slash is REQUIRED by Retrofit.
     */
    const val BASE_URL =
        "https://crusty-crab-api-tameez-hjechmamdneee7c6.southafricanorth-01.azurewebsites.net/"

    const val TIMEOUT_SECONDS = 30L
}