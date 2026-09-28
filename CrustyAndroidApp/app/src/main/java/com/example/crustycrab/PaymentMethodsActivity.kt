package com.example.crustycrab

import android.os.Bundle
import android.view.LayoutInflater
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.lifecycle.lifecycleScope
import com.example.crustycrab.databinding.ActivityPaymentMethodsBinding
import com.example.crustycrab.databinding.ItemPaymentMethodBinding
import com.example.crustycrab.repository.CrustyRepository
import com.example.crustycrab.util.ApiResult
import kotlinx.coroutines.launch

class PaymentMethodsActivity : AppCompatActivity() {

    private lateinit var binding: ActivityPaymentMethodsBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityPaymentMethodsBinding.inflate(layoutInflater)
        setContentView(binding.root)

        binding.btnBack.setOnClickListener { finish() }
        loadMethods()
    }

    private fun loadMethods() {
        lifecycleScope.launch {
            when (val r = CrustyRepository.getPaymentMethods()) {
                is ApiResult.Success -> {
                    binding.llMethods.removeAllViews()
                    val inflater = LayoutInflater.from(this@PaymentMethodsActivity)
                    r.data.forEach { pm ->
                        val row = ItemPaymentMethodBinding.inflate(
                            inflater, binding.llMethods, false
                        )
                        row.tvMethodName.text = pm.methodName
                        binding.llMethods.addView(row.root)
                    }
                }
                is ApiResult.Error -> toast(r.message)
            }
        }
    }

    private fun toast(m: String) =
        Toast.makeText(this, m, Toast.LENGTH_SHORT).show()
}