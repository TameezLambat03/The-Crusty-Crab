package com.example.crustycrab

import android.content.Intent
import android.os.Bundle
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.example.crustycrab.databinding.ActivityAccountBinding
import com.example.crustycrab.session.SessionManager

class AccountActivity : AppCompatActivity() {

    private lateinit var binding: ActivityAccountBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityAccountBinding.inflate(layoutInflater)
        setContentView(binding.root)

        // ---------- Profile (from session) ----------
        val name = SessionManager.getName(this).ifBlank { "Guest" }
        val email = SessionManager.getEmail(this).ifBlank { "not signed in" }
        val initials = name.split(" ")
            .mapNotNull { it.firstOrNull()?.uppercase() }
            .take(2)
            .joinToString("")

        binding.tvProfileName.text = name
        binding.tvProfileEmail.text = email
        binding.tvAvatar.text = initials.ifBlank { "?" }

        // Stat placeholders — will be populated later if you add counts
        binding.tvOrdersCount.text = "-"
        binding.tvFavoritesCount.text = "-"
        binding.tvReviewsCount.text = "-"

        // ---------- Top bar back ----------
        binding.ivAccBack.setOnClickListener { finish() }

        // ---------- Menu items ----------
        binding.tvMenuOrderHistory.setOnClickListener {
            startActivity(Intent(this, TrackerActivity::class.java))
        }

        binding.tvMenuFavorites.setOnClickListener {
            toast("No favorites yet — favorite items will appear here.")
        }

        binding.tvMenuDeliveryAddress.setOnClickListener {
            startActivity(Intent(this, DeliveryAddressActivity::class.java))
        }

        binding.tvMenuPaymentMethods.setOnClickListener {
            startActivity(Intent(this, PaymentMethodsActivity::class.java))
        }

        binding.tvMenuNotifications.setOnClickListener {
            toast("Notifications coming soon.")
        }

        binding.tvMenuHelpSupport.setOnClickListener {
            startActivity(Intent(this, CrabbyBotActivity::class.java))
        }

        binding.tvMenuLogout.setOnClickListener { handleLogout() }

        setupBottomNavigation()
    }

    private fun handleLogout() {
        SessionManager.clear(this)
        toast("Logged out")
        startActivity(
            Intent(this, LoginActivity::class.java)
                .addFlags(Intent.FLAG_ACTIVITY_NEW_TASK or Intent.FLAG_ACTIVITY_CLEAR_TASK)
        )
        finish()
    }

    @Suppress("DEPRECATION")
    private fun setupBottomNavigation() {
        val nav = binding.bottomNavWrapper

        nav.getChildAt(0).setOnClickListener {
            startActivity(Intent(this, HomeActivity::class.java))
            finish()
            overridePendingTransition(0, 0)
        }
        nav.getChildAt(1).setOnClickListener {
            startActivity(Intent(this, MenuActivity::class.java))
            finish()
            overridePendingTransition(0, 0)
        }
        nav.getChildAt(2).setOnClickListener {
            startActivity(Intent(this, CartActivity::class.java))
            overridePendingTransition(0, 0)
        }
        nav.getChildAt(3).setOnClickListener {
            startActivity(Intent(this, TrackerActivity::class.java))
            finish()
            overridePendingTransition(0, 0)
        }
        nav.getChildAt(4).setOnClickListener {
            // Already on Account — no-op
        }
    }

    private fun toast(m: String) =
        Toast.makeText(this, m, Toast.LENGTH_SHORT).show()
}