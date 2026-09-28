package com.example.crustycrab.api

object ApiConfig {
    /**
     * Base URL for the CrustyAPI backend.
     *
     * - Android emulator      → http://10.0.2.2:PORT/  (maps to host machine's localhost)
     * - Physical device       → http://<YOUR_PC_LAN_IP>:PORT/  (e.g. http://192.168.1.50:5083/)
     * - Production (HTTPS)    → https://api.crustycrab.co.za/
     *
     * NOTE: The trailing slash is REQUIRED by Retrofit.
     */
    const val BASE_URL = "http://10.0.2.2:5083/"

    const val TIMEOUT_SECONDS = 30L
}